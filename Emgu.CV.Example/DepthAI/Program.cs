﻿//----------------------------------------------------------------------------
//  Copyright (C) 2004-2021 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;

using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.DepthAI;
using Emgu.CV.ML;
using Emgu.CV.Models.DepthAI;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Newtonsoft.Json;


namespace DepthAI
{ 

    class Program
    {
        private static bool _is_running = true;
        private static Mat _render = new Mat();

        private static NNetPacket.Detection[] _mostRecentDetections;

        private static void DrawDetection(NNetPacket.Detection detection, Mat image, String[] labels = null)
        {
            float xMin = detection.XMin * image.Width;
            float xMax = detection.XMax * image.Width;
            float yMin = detection.YMin * image.Height;
            float yMax = detection.YMax * image.Height;
            
            RectangleF rectangleF = new RectangleF(xMin, yMin, xMax - xMin, yMax - yMin);
            Rectangle rectangle = Rectangle.Round(rectangleF);
            CvInvoke.Rectangle(image, rectangle, new MCvScalar(0, 0, 255), 2 );

            String text; 
            if (labels == null)
            {
                text = detection.Label.ToString();
            } else
            {
                text = labels[detection.Label];
            }
            CvInvoke.PutText(image, text, rectangle.Location, Emgu.CV.CvEnum.FontFace.HersheySimplex, 1.0, new MCvScalar(0, 0, 255));

        }

        private static void onDownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            if (e.TotalBytesToReceive <= 0)
                System.Console.WriteLine(String.Format("{0} bytes downloaded", e.BytesReceived, e.ProgressPercentage));
            else
                System.Console.WriteLine(String.Format("{0} of {1} bytes downloaded ({2}%)", e.BytesReceived, e.TotalBytesToReceive, e.ProgressPercentage));
        }

        static async Task Main(string[] args)
        {
            CvInvoke.Init();

            if (!DepthAIInvoke.HaveDepthAI)
            {
                Console.WriteLine("The native binary is built without Depth AI support.");
                return;
            }

            String win1 = "Depth AI (Press 'q' to quit)"; //The name of the window

            CvInvoke.NamedWindow(win1); //Create the window using the specific name

            //String blobFile = "D:\\sourceforge\\depthai\\resources\\nn/mobilenet-ssd/mobilenet-ssd.blob.sh14cmx14NCE1";
            //String blobFileConfig = "D:\\sourceforge\\depthai\\resources\\nn/mobilenet-ssd/mobilenet-ssd.json";

            //Config config = Emgu.CV.Models.DepthAI.MobilenetSsd.GetConfig(blobFile, blobFileConfig);

            MobilenetSsd mobilenet = new MobilenetSsd();
            Config config = await mobilenet.Init(onDownloadProgressChanged);
            String configStr = JsonConvert.SerializeObject(config);
            String[] labels = mobilenet.Labels;

            using (Emgu.CV.DepthAI.Device d = new Device(""))
            {
                //String[] availableStreams = d.GetAvailableStreams();
                using (CNNHostPipeline pipeline = d.CreatePipeline(configStr))
                {
                    while (_is_running)
                    {
                        using (NNetAndDataPackets packets = pipeline.GetAvailableNNetAndDataPackets(false))
                        {
                            HostDataPacket[] dataPackets = packets.HostDataPackets;
                            NNetPacket[] nnetPackets = packets.NNetPackets;

                            for (int i = 0; i < dataPackets.Length; i++)
                            {
                                if (dataPackets[i].GetPreviewOut(_render))
                                {
                                    using (FrameMetadata fmeta = dataPackets[i].GetFrameMetadata())
                                    {
                                        if (i < nnetPackets.Length)
                                        {
                                            NNetPacket nnetPacket = nnetPackets[i];
                                            using (FrameMetadata meta = nnetPacket.GetFrameMetadata())
                                            {
                                                
                                                _mostRecentDetections = nnetPacket.Detections;
                                            }
                                        }

                                        if (_mostRecentDetections != null)
                                        {
                                            for (int j = 0; j < _mostRecentDetections.Length; j++)
                                            {
                                                DrawDetection(_mostRecentDetections[j], _render, labels);
                                            }
                                        }

                                        CvInvoke.Imshow(win1, _render); //Show the image
                                    }
                                }
                                else
                                {
                                }
                                
                            }
                        }

                        if (CvInvoke.WaitKey(1) > 0) //Press any key to stop
                        {
                            _is_running = false;
                        }
                    }
                }
            }
            
            CvInvoke.DestroyAllWindows(); //Destroy all windows if key is pressed
            
        }
    }
}