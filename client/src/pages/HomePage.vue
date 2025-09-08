<script setup >
import { AppState } from '@/AppState';
import Banner from '@/components/Banner.vue';
import CreateRecipe from '@/components/CreateRecipe.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import RecipeModal from '@/components/RecipeModal.vue';
import { favoriteService } from '@/services/FavoriteService';
import { recipeService } from '@/services/RecipeService';
import { watch } from 'vue';

  watch(() => AppState.identity, async(identity) => {
    if(!identity) return;
      favoriteService.getFavorites();
    });
    recipeService.getRecipes();

</script>

<template>
  <div class="container">
    <Banner/>
    <CreateRecipe />
    <div class="px-4 d-flex justify-content-between flex-wrap">
      <RecipeCard v-for="recipe in AppState.recipes" :key="recipe.id" :recipe="recipe" style="width:28%; margin-bottom:.5rem;" />
    </div>
    <RecipeModal />
  </div>
</template>

<style scoped lang="scss">

</style>
