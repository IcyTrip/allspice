import { allSpiceApi } from "@/env.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Favorite } from "@/models/Favorite.js";

class FavoriteService{

    async getAllFavorites() {
        const res = await api.get(`${allSpiceApi}/api/Favorites`);
        AppState.allFavorites.length = 0;
        res.data.forEach(favorite => {
            AppState.allFavorites.unshift(new Favorite(favorite));
        });
    }

    async getFavorites() {
        const res = await api.get(`${allSpiceApi}/api/Favorites`);
        AppState.favorites.length = 0;
        res.data.forEach(favorite => {
            if(favorite.accountId !== AppState.account?.sub) return;
            AppState.favorites.unshift(new Favorite(favorite));
            
            const matchingRecipe = AppState.recipes.find(r => r.id === favorite.recipeId);
            if(matchingRecipe) {
                matchingRecipe.favorite = true;
            }
        });
        await this.getAllFavorites();
    }

    async createFavorite(data) {
        const res = await api.post(`${allSpiceApi}/api/Favorites`, data);
        await this.getFavorites();
        return res.data;
    }

    async getFavoriteById(id) {
        const res = await api.get(`${allSpiceApi}/api/Favorites/${id}`);
        return res.data;
    }

    async deleteFavorite(id) {
        await api.delete(`${allSpiceApi}/api/Favorites/${id}`);
        const favorite = AppState.favorites.find(f => f.id === id);

        AppState.favorites = AppState.favorites.filter(f => f.id !== id);
        
        const matchingRecipe = AppState.recipes.find(r => r.id === favorite.recipeId);
        if(matchingRecipe) {
            matchingRecipe.favorite = false;
        }
        await this.getFavorites();
    }
}

export const favoriteService = new FavoriteService();