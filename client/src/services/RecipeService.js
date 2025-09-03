import { allSpiceApi } from "@/env.js";
import { api } from "./AxiosService.js";

class RecipeService{
    async createRecipe(data) {
        const res = await api.post(`${allSpiceApi}/api/Recipe`, data);
        return res.data;
    }
}

export const recipeService = new RecipeService();