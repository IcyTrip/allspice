<script setup >
import { AppState } from '@/AppState';
import Banner from '@/components/Banner.vue';
import CreateRecipe from '@/components/CreateRecipe.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import RecipeModal from '@/components/RecipeModal.vue';
import { recipeService } from '@/services/RecipeService';
import { watch } from 'vue';

  watch(() => AppState.identity, async(identity) => {
    if(!identity) return;
    const testRecipe = {
      title: "Test recipe",
      instructions: "these are the instructions.",
      img: "https://content.health.harvard.edu/wp-content/uploads/2023/07/b8a1309a-ba53-48c7-bca3-9c36aab2338a.jpg",
      category: 4,
      creatorId: AppState.identity?.sub
    }

    
    // try{
      //   const recipe = await recipeService.createRecipe(testRecipe);
      //   console.log("Recipe created: ", recipe);
      // } catch(err) {
        //   console.error("Error creating recipe", err);
        // }
      });
      recipeService.getRecipes();

</script>

<template>
  <div class="container">
    <Banner/>
    <CreateRecipe />
    <div class="px-4 d-flex justify-content-between flex-wrap">
      <RecipeCard @click="console.log('clicked')" v-for="recipe in AppState.recipes" :key="recipe.id" :recipe="recipe" style="width:28%; margin-bottom:5.5rem;" />
    </div>
    <RecipeModal />
  </div>
</template>

<style scoped lang="scss">

</style>
