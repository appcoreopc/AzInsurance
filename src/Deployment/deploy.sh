az group create --name "TestDevGroup" --location australiasoutheast

az servicebus namespace create --resource-group "TestDevGroup" --name "DevServiceSpace" --location australiasoutheast

az servicebus queue create --resource-group "TestDevGroup" --namespace-name "DevServiceSpace" --name "ClaimQueue"


