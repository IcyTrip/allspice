import { allSpiceApi } from "@/env.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Ingredient } from "@/models/Ingredient.js";

class IngredientService{
    async getIngredients() {
        const res = await api.get(`${allSpiceApi}/api/Ingredients`);
        AppState.ingredients.length = 0;
        res.data.forEach(ingredient => {
            AppState.ingredients.unshift(new Ingredient(ingredient));
        });
    }

    async createIngredient(data) {
        const res = await api.post(`${allSpiceApi}/api/Ingredients`, data);
        await this.getIngredients();
        return res.data;
    }

    async getIngredientById(id) {
        const res = await api.get(`${allSpiceApi}/api/Ingredients/${id}`);
        return res.data;
    }

    async getAllIngredientsById(id) {
        const res = await api.get(`${allSpiceApi}/api/Ingredients/recipe${id}`);
        return res.data;
    }

    async deleteIngredient(id) {
        await api.delete(`${allSpiceApi}/api/Ingredients/${id}`);
        AppState.ingredients = AppState.ingredients.filter(i => i.id !== id);
    }
}

export const ingredientService = new IngredientService;