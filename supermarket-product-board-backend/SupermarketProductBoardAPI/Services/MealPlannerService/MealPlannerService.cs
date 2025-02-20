using Microsoft.EntityFrameworkCore;
using OpenAI.Chat;
using SupermarketProductBoardAPI.Data;
using SupermarketProductBoardAPI.Models.DTOs;
using System.Text.Json;

namespace SupermarketProductBoardAPI.Services.MealPlannerService
{
    public class MealPlannerService : IMealPlannerService
    {
        private readonly AppDbContext context;
        private readonly string openAiApiKey;


        public MealPlannerService(AppDbContext context, IConfiguration configuration)
        {
            this.context = context;
            openAiApiKey = configuration.GetRequiredSection("AppSecrets:OpenApiKey").Value
                ?? throw new ArgumentNullException("OpenAiApiKey is not configured.");
        }

        public async Task<MealPlanCreateResponse> CreateMealPlan(MealPlanCreateRequest mealPlanRequest)
        {

            /// Prepare product list


            // Get the active papers that the user wants to use for the meal plan
            var newestPaperId = await context.Papers
                .Where(x => mealPlanRequest.SuperMarketId == x.CompanyId && x.StartDate <= DateOnly.FromDateTime(DateTime.Now) && x.EndDate >= DateOnly.FromDateTime(DateTime.Now))
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();


            // Get all categories
            var categories = await context.Categories.ToListAsync();

            // Get the products from the active papers
            var products = await context.Products
               .Where(x => x.ShowOnline && x.PaperId.HasValue && newestPaperId == x.PaperId.Value)
               .Select(x => new
               {
                   x.Id,
                   x.Name,
                   x.Price,
                   x.Amount,
                   x.Description,
                   CategoryName = x.Category != null ? x.Category.Name : null, // Get category name
                   x.ShowOnline
               })
               .ToListAsync();

            // Seralize it for the GPT-4o model
            var json = JsonSerializer.Serialize(products, new JsonSerializerOptions
            {
                WriteIndented = true // Optional: Makes the JSON more readable
            });

            if (string.IsNullOrEmpty(json))
            {
                throw new Exception("Error in serializing products");
            }

            var mealPlan = CreateMealPlanGPT(json, mealPlanRequest);

            return mealPlan;
        }

        public MealPlanCreateResponse CreateMealPlanGPT(string products, MealPlanCreateRequest request)
        {
            ChatClient client = new(model: "gpt-4o", apiKey: openAiApiKey);
            ChatCompletionOptions options = new()
            {
                ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                    jsonSchemaFormatName: "meal_plan",
                    jsonSchema: BinaryData.FromBytes("""
            {
                "type": "object",
                "properties": {
                    "days": {
                        "type": "array",
                        "items": {
                            "type": "object",
                            "properties": {
                                "day": { "type": "string" },
                                "meal": { "type": "string" },
                                "ingredients": {
                                    "type": "array",
                                    "items": {
                                        "type": "object",
                                        "properties": {
                                            "name": { "type": "string" },
                                            "category": { "type": "string" },
                                            "price": { "type": "string" },
                                            "amount": { "type": "string"}
                                        },
                                        "required": ["name", "price", "category", "amount"],
                                        "additionalProperties": false
                                    }
                                },
                                "preparation": { "type": "string" }
                            },
                            "required": ["day", "meal", "ingredients", "preparation"],
                            "additionalProperties": false
                        }
                    }
                },
                "required": ["days"],
                "additionalProperties": false
            }
            """u8.ToArray()),
                        jsonSchemaIsStrict: true)
            };

            string systemMessage = File.ReadAllText("Utils/system_prompt_meal_plan_create.txt");
            var preferredIngredients = request.PreferedIngredients != null && request.PreferedIngredients.Count() > 0 ? string.Join(", ", request.PreferedIngredients) : "None";

            ChatCompletion completion = client.CompleteChat(
                [
                    new SystemChatMessage(systemMessage),
                    new UserChatMessage($"Products: {products}. People: {request.AmountOfPeople}. Days: {request.Days}. Dietary preference: {request.MealType}. Preferred Ingredients: {preferredIngredients}. Freshness Preference: {request.FreshnessPreference}")
                ], options);

            using JsonDocument structuredJson = JsonDocument.Parse(completion.Content[0].Text);

            MealPlanCreateResponse? mealPlan = JsonSerializer.Deserialize<MealPlanCreateResponse>(completion.Content[0].Text);

            if (mealPlan == null)
            {
                throw new Exception("Error in deserializing meal plan");
            }

            return mealPlan;
        }
    }
}
