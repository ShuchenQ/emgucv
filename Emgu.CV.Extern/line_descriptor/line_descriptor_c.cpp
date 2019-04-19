//----------------------------------------------------------------------------
//
//  Copyright (C) 2004-2019 by EMGU Corporation. All rights reserved.
//
//----------------------------------------------------------------------------

/*

#include "line_descriptor_c.h"

cv::line_descriptor::BinaryDescriptor* cveLineDescriptorBinaryDescriptorCreate(cv::Ptr<cv::line_descriptor::BinaryDescriptor>** sharedPtr)
{
	cv::Ptr<cv::line_descriptor::BinaryDescriptor> ptr = cv::line_descriptor::BinaryDescriptor::createBinaryDescriptor();
	*sharedPtr = new cv::Ptr<cv::line_descriptor::BinaryDescriptor>(ptr);
	return ptr.get();
}
void cveLineDescriptorBinaryDescriptorDetect(cv::line_descriptor::BinaryDescriptor* descriptor, cv::Mat* image, std::vector<cv::line_descriptor::KeyLine>* keypoints, cv::Mat* mask)
{
	descriptor->detect(*image, *keypoints, mask ? *mask : cv::Mat());
}
void cveLineDescriptorBinaryDescriptorCompute(cv::line_descriptor::BinaryDescriptor* descriptor, cv::Mat* image, std::vector<cv::line_descriptor::KeyLine>* keylines, cv::Mat* descriptors, bool returnFloatDescr)
{
	descriptor->compute(*image, *keylines, *descriptors, returnFloatDescr);
}

void cveLineDescriptorBinaryDescriptorRelease(cv::Ptr<cv::line_descriptor::BinaryDescriptor>** sharedPtr)
{
	delete *sharedPtr;
	*sharedPtr = 0;
}

cv::line_descriptor::LSDDetector* cveLineDescriptorLSDDetectorCreate(cv::Ptr<cv::line_descriptor::LSDDetector>** sharedPtr)
{
	cv::Ptr<cv::line_descriptor::LSDDetector> ptr = cv::line_descriptor::LSDDetector::createLSDDetector();
	*sharedPtr = new cv::Ptr<cv::line_descriptor::LSDDetector>(ptr);
	return ptr.get();
}
void cveLineDescriptorLSDDetectorDetect(cv::line_descriptor::LSDDetector* detector, cv::Mat* image, std::vector<cv::line_descriptor::KeyLine>* keypoints, int scale, int numOctaves, cv::Mat* mask)
{
	detector->detect(*image, *keypoints, scale, numOctaves, mask ? *mask : cv::Mat());
}
void cveLineDescriptorLSDDetectorRelease(cv::Ptr<cv::line_descriptor::LSDDetector>** sharedPtr)
{
	delete *sharedPtr;
	*sharedPtr = 0;
}

*/