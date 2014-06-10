public class JSONDataModelBinder<T> : IModelBinder
{
    private string valueObjName { get; set; }

    public JSONDataModelBinder(string objName) 
    {
        valueObjName = objName;
    }
    
    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
        // Get the raw attempted value from the value provider
        string incomingData = bindingContext.ValueProvider.GetValue(valueObjName).AttemptedValue;

        if (!string.IsNullOrEmpty(incomingData))
        {
            // convert string to stream (2nd method)
            //byte[] byteArray = Encoding.UTF8.GetBytes(incomingData);
            //MemoryStream stream = new MemoryStream(byteArray);
            //object o = new DataContractJsonSerializer(typeof(DailyLogSaveMobile)).ReadObject(stream);
            
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            var model = json_serializer.Deserialize<T>(incomingData);

            return model;
        }

        return null;
    }
}
