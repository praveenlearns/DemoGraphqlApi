# DemoGraphqlApi
 
Please follow these steps to test your first graphql api yourself. You can then debug easily to understand better. You should have VS2017 to run this sample and latest Postman tool to test graphql api. Please remember that postman doesn’t have intellisense yet.

1.	Please download source code from github. 
2.	Open the solution file “DemoGraphqlApi.sln”
3.	Press F5. You should see your default browser running the url http://localhost:1480/api
4.	Browse to the file named “GraphQL Demo.postman_collection.json” in file explorer. This has the sample request which is ready to run.
5.	Open postman tool.
6.	Click on “Import” button on top left. It prompts you to select a collection file. 
7.	Select the json file mentioned in step 4.
8.	You will see a collection folder named “GraphQL Demo” on left pane. Expand it.
9.	Select the query “Testing GraphQL Query”. Click body tab on right pane.
10.	You should see query on left and variable on right
11.	Notice that it’s a POST call and graphql endpoint is http://localhost:1480/api/graphql
12.	Hit Send button.
13.	You should see response below.
