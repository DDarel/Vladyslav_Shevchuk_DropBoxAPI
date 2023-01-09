Feature: HomeWork

@1first
Scenario: Upload
Given Output User info
When Upload file
Then Check file is uploaded

@2cecond
Scenario: GetFileMetaData
Given Upload file
When GetMetaData
Then ShowMetaData

@3third
Scenario: Delete file
Given Upload file
When DeleteFile
Then Check file is deleted

