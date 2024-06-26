# --------------------------------------------------------
#  Copyright (C) 2004-2024 by EMGU Corporation. All rights reserved.
# --------------------------------------------------------

IF((TARGET Emgu.CV.Platform.Maui.Mac) AND HAVE_MACOS)

  PROJECT(Emgu.CV.runtime.maui.macos.nuget)

  get_filename_component(NUGET_OUTPUT_DIR ${CMAKE_CURRENT_SOURCE_DIR} DIRECTORY)

  BUILD_NUGET_PACKAGE(
    ${PROJECT_NAME} 
	"${CMAKE_CURRENT_SOURCE_DIR}/../../../Emgu.CV.Platform/NetStandard/Emgu.CV.Platform.NetStandard.csproj"  #csproj_file 
	"${CMAKE_CURRENT_SOURCE_DIR}/Package.nuspec" #nuspec_file 
	"${NUGET_OUTPUT_DIR}" #output_dir 
	"${CMAKE_CURRENT_SOURCE_DIR}" #working_dir
	) 

  ADD_DEPENDENCIES(${PROJECT_NAME} cvextern)
  
  IF (TARGET Emgu.CV.Platform.Maui.Mac)
    ADD_DEPENDENCIES(${PROJECT_NAME} Emgu.CV.Platform.Maui.Mac)
  ENDIF()
  
  IF (TARGET Emgu.CV.nuget)
    ADD_DEPENDENCIES(${PROJECT_NAME} Emgu.CV.nuget)
  ENDIF()

  IF(ENABLE_SOLUTION_FOLDERS)
    set_target_properties(${PROJECT_NAME} PROPERTIES FOLDER "nuget")
  ENDIF() 

  IF (EMGU_NUGET_SIGN_FOUND)
    EMGU_SIGN_NUGET(${PROJECT_NAME} "${NUGET_OUTPUT_DIR}/${EMGUCV_MACOS_NUGET_ID}.${CPACK_PACKAGE_VERSION}.nupkg")
  ENDIF()



ENDIF()
