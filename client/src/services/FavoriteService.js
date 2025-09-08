import { allSpiceApi } from "@/env.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Favorite } from "@/models/Favorite.js";

class FavoriteService{

    async getFavorites() {
        const res = await api.get(`${allSpiceApi}/api/Favorite`);
        AppState.favorites.length = 0;
        res.data.forEach(favorite => {
            if(favorite.accountId !== AppState.account?.sub) return;
            AppState.favorites.unshift(new Favorite(favorite));
            
            const matchingRecipe = AppState.recipes.find(r => r.id === favorite.recipeId);
            if(matchingRecipe) {
                matchingRecipe.favorite = true;
            }
        });
    }

    async createFavorite(data) {
        const res = await api.post(`${allSpiceApi}/api/Favorite`, data);
        await this.getFavorites();
        return res.data;
    }

    async getFavoriteById(id) {
        const res = await api.get(`${allSpiceApi}/api/Favorite/${id}`);
        return res.data;
    }

    async deleteFavorite(id) {
        await api.delete(`${allSpiceApi}/api/Favorite/${id}`);
        const favorite = AppState.favorites.find(f => f.id === id);

        AppState.favorites = AppState.favorites.filter(f => f.id !== id);
        
        const matchingRecipe = AppState.recipes.find(r => r.id === favorite.recipeId);
        if(matchingRecipe) {
            matchingRecipe.favorite = false;
        }
    }
}

export const favoriteService = new FavoriteService();