<script setup >
import { AppState } from '@/AppState';
import { recipeService } from '@/services/RecipeService';
import { onMounted } from 'vue';

  onMounted(async () => {
    if(!AppState.identity) {
      return;
    }
  });

  const testRecipe = {
    title: "Test recipe",
    instructions: "these are the instructions.",
    img: "https://content.health.harvard.edu/wp-content/uploads/2023/07/b8a1309a-ba53-48c7-bca3-9c36aab2338a.jpg",
    category: 4,
    creatorId: AppState.identity?.sub
  }

  try{
    const recipe = await recipeService.createRecipe(testRecipe);
    console.log("Recipe created: ", recipe);
  } catch(err) {
    console.error("Error creating recipe", err);
  }
</script>

<template>
  <div></div>
</template>

<style scoped lang="scss">

</style>
