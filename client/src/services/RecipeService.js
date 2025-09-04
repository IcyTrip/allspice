import { allSpiceApi } from "@/env.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Recipe } from "@/models/Recipe.js";

class RecipeService{

    async getRecipes() {
        const res = await api.get(`${allSpiceApi}/api/Recipe`);
        AppState.recipes.length = 0;
        res.data.forEach(recipe => {
            AppState.recipes.push(new Recipe(recipe));
        });
    }

    async createRecipe(data) {
        const res = await api.post(`${allSpiceApi}/api/Recipe`, data);
        return res.data;
    }

    async deleteRecipe(id) {
        await api.delete(`${allSpiceApi}/api/Recipe/${id}`);
        AppState.recipes = AppState.recipes.filter(r => r.id !== id);
    }

    async updateRecipe(id, title, category, img, instructions) {
        await api.put(`${allSpiceApi}/api/Recipe/${id}`, { title, category, img, instructions });
        await this.getRecipes();
    }

    isValidUrl(url) {
        try{
            new URL(url);
            return true;
        } catch {
            return false;
        }
    }
}

export const recipeService = new RecipeService();