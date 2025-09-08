import { AppState } from '../AppState.js'
import { logger } from '../utils/Logger.js'
import { api } from './AxiosService.js'
import { favoriteService } from './FavoriteService.js';

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/Account')
      AppState.account = res.data;

      AppState.favorites.length = 0;
      await favoriteService.getFavorites();
      AppState.recipes.forEach(recipe => {
        recipe.favorite = AppState.favorites.some(f => f.recipeId === recipe.id);
      })
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err);
    }
  }
}

export const accountService = new AccountService()
