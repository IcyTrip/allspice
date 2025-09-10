import { allSpiceApi } from "@/env.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Ingredient } from "@/models/Ingredient.js";

class IngredientService{
    async getIngredients() {
        const res = await api.get(`${allSpiceApi}/api/Ingredient`);
        AppState.ingredients.length = 0;
        res.data.forEach(ingredient => {
            AppState.ingredients.unshift(new Ingredient(ingredient));
        });
    }

    async createIngredient(data) {
        const res = await api.post(`${allSpiceApi}/api/Ingredient`, data);
        await this.getIngredients();
        return res.data;
    }

    async getIngredientById(id) {
        const res = await api.get(`${allSpiceApi}/api/Ingredient/${id}`);
        return res.data;
    }

    async getAllIngredientsById(id) {
        const res = await api.get(`${allSpiceApi}/api/Ingredient/recipe${id}`);
        return res.data;
    }

    async deleteIngredient(id) {
        await api.delete(`${allSpiceApi}/api/Ingredient/${id}`);
        AppState.ingredients = AppState.ingredients.filter(i => i.id !== id);
    }
}

export const ingredientService = new IngredientService;