using OpenAI;
using OpenAI.Responses;


namespace rodri_movie_mvc.Service
{
    public class LlmService
    {
        private readonly string _apiKey;
        private readonly string _model;

        public LlmService()
        {
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
                ?? throw new InvalidOperationException("La variable de entorno 'OPENAI_API_KEY' no está configurada.");
            _model = "gpt-4o-mini";
        }

        public async Task<string> ObtenerSpoilerAsync(string tituloPelicula)
        {
            if (string.IsNullOrWhiteSpace(tituloPelicula))
                throw new ArgumentException("El título de la película no puede estar vacío.", nameof(tituloPelicula));

            string prompt = $@"Genera un pequeño spoiler (máximo 2-3 oraciones) sobre la película ""{tituloPelicula}"". 
                        El spoiler debe revelar algún giro interesante de la trama sin arruinar completamente la experiencia. 
                        Sé conciso y cautivador.";

            return await ConsultarLlmAsync(prompt);
        }


        public async Task<string> ObtenerResumenAsync(string tituloPelicula)
        {
            if (string.IsNullOrWhiteSpace(tituloPelicula))
                throw new ArgumentException("El título de la película no puede estar vacío.", nameof(tituloPelicula));

            string prompt = $@"Proporciona un resumen breve (máximo 3-4 oraciones) de la película ""{tituloPelicula}"". 
                        Incluye el género, la premisa principal y por qué es relevante o interesante. 
                        No incluyas spoilers importantes.";

            return await ConsultarLlmAsync(prompt);
        }


        private async Task<string> ConsultarLlmAsync(string prompt)
        {
            try
            {
                OpenAIResponseClient client = new(
                    model: _model,
                    apiKey: _apiKey
                );

                OpenAIResponse response = await client.CreateResponseAsync(prompt);
                string assistantResponse = response.GetOutputText();

                return assistantResponse?.Trim() ?? "No se pudo obtener una respuesta.";
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException($"Error de conexión con la API de OpenAI: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al procesar la solicitud: {ex.Message}", ex);
            }
        }

        public async Task<string> ConsultaSimpleAsync(string pregunta)
        {
            if (string.IsNullOrWhiteSpace(pregunta))
                throw new ArgumentException("La pregunta no puede estar vacía.", nameof(pregunta));

            return await ConsultarLlmAsync(pregunta);
        }
    }
}