import { allSpiceApi } from "@/env.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Recipe } from "@/models/Recipe.js";

class RecipeService{

    async getRecipes() {
        const res = await api.get(`${allSpiceApi}/api/Recipe`);
        AppState.recipes.length = 0;
        res.data.forEach(recipe => {
            const newRecipe = new Recipe(recipe);
            if(AppState.favorites.find(f => f.recipeId === recipe.id)) {
                newRecipe.favorite = true;
            }
            AppState.recipes.unshift(newRecipe);
        });
    }
    
    async getUserRecipes() {
        const res = await api.get(`${allSpiceApi}/api/Recipe`);
        AppState.recipes.length = 0;
        res.data.forEach(recipe => {
            const newRecipe = new Recipe(recipe);
            if(AppState.favorites.find(f => f.recipeId === recipe.id)) {
                newRecipe.favorite = true;
            }
            if(AppState.account.sub === recipe.creatorId) {
                AppState.recipes.unshift(newRecipe);
            }
        })
    }

    async getFavoriteRecipes() {
        const res = await api.get(`${allSpiceApi}/api/Recipe`);
        AppState.recipes.length = 0;
        res.data.forEach(recipe => {
            AppState.favorites.forEach(favorite => {
                if(recipe.id === favorite.recipeId) {
                    const newRecipe = new Recipe(recipe);
                    newRecipe.favorite = true;
                    AppState.recipes.unshift(newRecipe);
                }
            });
        });
    }

    async getRecipeById(id) {
        const res = await api.get(`${allSpiceApi}/api/Recipe/${id}`);
        return res.data;
    }

    async createRecipe(data) {
        const res = await api.post(`${allSpiceApi}/api/Recipe`, data);
        await this.getRecipes();
        return res.data;
    }

    async deleteRecipe(id) {

        await api.delete(`${allSpiceApi}/api/Recipe/${id}`);
        AppState.recipes = AppState.recipes.filter(r => r.id !== id);
        AppState.favorites = AppState.favorites.filter(f => f.recipeId !== id);
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

    convertToCategory(id) {
        switch(id+1) {
            case 1:
                return "Breakfast";
            case 2:
                return "Lunch";
            case 3:
                return "Dinner";
            case 4:
                return "Snack";
            case 5:
                return "Dessert";
            default:
                return "Miscellaneous";
        }
    }
}

export const recipeService = new RecipeService();