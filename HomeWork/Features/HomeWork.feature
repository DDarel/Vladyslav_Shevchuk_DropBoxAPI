Feature: HomeWork

@1first
Scenario: Upload
Given Output User info
When Upload file
Then Show succes message

@2cecond
Scenario: GetFileMetaData
Given CheckFileAvailable
When GetMetaData
Then ShowMetaData

@3third
Scenario: Delete file
Given CheckFileAvailable
When DeleteFile
Then Show delete succes message

