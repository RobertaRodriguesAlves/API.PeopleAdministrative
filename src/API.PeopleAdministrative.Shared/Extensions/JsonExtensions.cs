using JsonNet.ContractResolvers;
using Newtonsoft.Json;

namespace API.PeopleAdministrative.Shared.Extensions;

// Extensão para Json
public static class JsonExtensions
{
    private static readonly PrivateSetterContractResolver ContractResolver = new PrivateSetterContractResolver();
    private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings().Configure();

    // 
    // Summary:
    //  Desserializa o JSON para o tipo especificado.
    // 
    // Parameters:
    //  value:
    //      O objeto a ser desserializado.
    //
    // Type parameters:
    //  T:
    //      O tipo de objeto para o qual desserializar.
    //
    // Returns:
    //     O objeto desserializado da string JSON.
    public static T FromJson<T>(this string value)
    {
        return (T)((value != null) ? ((object)JsonConvert.DeserializeObject<T>(value, JsonSettings)) : (default(T)));
    }

    // 
    // Summary:
    //  Serializa o objeto especificado em uma string JSON.
    // 
    // Parameters:
    //  value:
    //      O objeto a ser serializado.
    //
    // formatting:
    //      A formatação da string JSON.
    //
    // Returns:
    //     Uma representação de string JSON do objeto.
    public static string ToJson<T>(this T value, Formatting formatting = Formatting.None)
    {
        return (value != null) ? JsonConvert.SerializeObject(value, formatting, JsonSettings) : null;
    }

    //
    // Summary:
    //      Aplica a configuração padrão no Serializador/Desserializador.
    //
    // Parameters:
    //  settings:
    public static JsonSerializerSettings Configure(this JsonSerializerSettings settings)
    {
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        settings.PreserveReferencesHandling = PreserveReferencesHandling.None;
        settings.NullValueHandling = NullValueHandling.Ignore;
        settings.Formatting = Formatting.None;
        settings.ContractResolver = ContractResolver;
        return settings;
    }
}
