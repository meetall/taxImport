# TaxImport Utility

###1. Description

This tool is built to process csv file and xlsx file with fixed format:

* Account
* Description
* CurrencyCode
* Value

The data could be processed and imported into SQL database.

###2. Assumptions

The tool assumes input data format are fixed. To deal with more formats, code could be modified according to the requirements.

####2.1 csv file

Csv file uses comma separator and the order of the format of the information must be fixed.

eg:

Account0,Description0,GBP,0

Account1,Description1,GBP,1

####2.2 xlsx file

The file should have 4 columns with headers as.

| Account | Description | CurrencyCode | Value |
|---------|-------------|--------------|-------|

All the data must be put into the worksheet "Data".

####2.3 Example data

There are two example files in TestData.

There is a commented out unit test created for generating csv test file. Uncomment [TestMethod] and then you would be able to run the unit test and create test data.

###3. Use the application

After the code is cloned to your local computer, you can build it with visual studio 2013. There are two nuget packages used for this solution. Normally they should be downloaded automatically. If not, please check the nuget settings and restore the two packages.

####3.1 Create SQL database
The application needs SQL database to run.

The following steps deploys the database.

1. Login to the SQL server with SQL Server Management Studio 
2. Create a empty database named "TaxInfo"
3. Modify the connection string in App.config. You can find this file in visual studio if the solution is open.
```
<connectionStrings>
    <add name="TaxModelContainer" connectionString="metadata=res://*/DB.TaxModel.csdl|res://*/DB.TaxModel.ssdl|res://*/DB.TaxModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=GBCL4V5VFV1;initial catalog=TaxInfo;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
  </connectionStrings>
```
4. Connect to SQL database in visual studio and run the script to create the database.

