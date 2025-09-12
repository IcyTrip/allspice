import { allSpiceApi } from "@/env.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Recipe } from "@/models/Recipe.js";

class RecipeService{

    async getRecipes() {
        const res = await api.get(`${allSpiceApi}/api/Recipes`);
        AppState.recipes.length = 0;
        res.data.forEach(recipe => {
            if(AppState.searchCat === 6 || AppState.searchCat === recipe.category) {
                const newRecipe = new Recipe(recipe);
                if(AppState.favorites.find(f => f.recipeId === recipe.id)) {
                    newRecipe.favorite = true;
                }
                AppState.recipes.unshift(newRecipe);
            }
            AppState.activeFilter = 0;
        });
    }
    
    async getUserRecipes() {
        const res = await api.get(`${allSpiceApi}/api/Recipes`);
        AppState.recipes.length = 0;
        res.data.forEach(recipe => {
            if(AppState.account?.sub === recipe.creatorId && (AppState.searchCat === 6 || AppState.searchCat === recipe.category)) {
                const newRecipe = new Recipe(recipe);
                if(AppState.favorites.find(f => f.recipeId === recipe.id)) {
                    newRecipe.favorite = true;
                }
                AppState.recipes.unshift(newRecipe);
            }
        });
        AppState.activeFilter = 1;
    }

    async getFavoriteRecipes() {
        const res = await api.get(`${allSpiceApi}/api/Recipes`);
        AppState.recipes.length = 0;
        res.data.forEach(recipe => {
            AppState.favorites.forEach(favorite => {
                if(recipe.id === favorite.recipeId && (AppState.searchCat === 6 || AppState.searchCat === recipe.category)) {
                    const newRecipe = new Recipe(recipe);
                    newRecipe.favorite = true;
                    AppState.recipes.unshift(newRecipe);
                }
            });
        });
        AppState.activeFilter = 2;
    }

    async searchRecipes(search) {
        const res = await api.get(`${allSpiceApi}/api/Recipes`);
        AppState.recipes.length = 0;
        res.data.forEach(recipe => {
            if(recipe.title.toLowerCase().includes(search.toLowerCase()) || search === '') {
                if(AppState.searchCat === 6 || AppState.searchCat === recipe.category) {
                    const newRecipe = new Recipe(recipe);
                    if(AppState.favorites.find(f => f.recipeId === recipe.id)) {
                        newRecipe.favorite = true;
                    }
                    AppState.recipes.unshift(newRecipe);
                }
            }
        });
        AppState.activeFilter = 3;
    }

    async getRecipeById(id) {
        const res = await api.get(`${allSpiceApi}/api/Recipes/${id}`);
        return res.data;
    }

    async createRecipe(data) {
        const res = await api.post(`${allSpiceApi}/api/Recipes`, data);
        await this.getRecipes();
        return res.data;
    }

    async deleteRecipe(id) {

        await api.delete(`${allSpiceApi}/api/Recipes/${id}`);
        AppState.recipes = AppState.recipes.filter(r => r.id !== id);
        AppState.favorites = AppState.favorites.filter(f => f.recipeId !== id);
    }

    async updateRecipe(id, instructions) {
        await api.put(`${allSpiceApi}/api/Recipes/${id}`, { instructions: instructions });
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

    setActiveCategory(cat) {
        switch(cat) {
            case 'All Categories':
                AppState.searchCat = 6
                break;
            case 'Breakfast':
                AppState.searchCat = 0
                break;
            case 'Lunch':
                AppState.searchCat = 1
                break;
            case 'Dinner':
                AppState.searchCat = 2
                break;
            case 'Snack':
                AppState.searchCat = 3
                break;
            case 'Dessert':
                AppState.searchCat = 4
                break;
            default:
                AppState.searchCat = 6
        }
    }
}

export const recipeService = new RecipeService();