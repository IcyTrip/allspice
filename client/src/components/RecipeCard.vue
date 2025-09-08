<script setup>
import { AppState } from '@/AppState';
import { favoriteService } from '@/services/FavoriteService';
import { recipeService } from '@/services/RecipeService';
import { logger } from '@/utils/Logger';

    const props = defineProps({
        recipe: {
            type: Object,
            required: true
        }
    });

    function convertToCategory(id) {
        try{
            return recipeService.convertToCategory(id);
        } catch(err) {
            logger.error("Could not convert to category",err);
        }
    }

    function deleteRecipe() {
        try{
            recipeService.deleteRecipe(props.recipe.id);
        } catch(err) {
            logger.error("Could not delete recipe",err);
        }
    }

    function favorite() {
        try{
            favoriteService.createFavorite({ recipeId: props.recipe.id });
        } catch(err) {
            logger.error("Could not favorite this recipe",err);
        }
    }

    function unfavorite() {
        try{
            const favorite = AppState.favorites.find(f => f.recipeId === props.recipe.id)
            if(!favorite) return;

            favoriteService.deleteFavorite(favorite.id);
        } catch(err) {
            logger.error("Could not unfavorite this recipe", err);
        }
    }
</script>

<template>
    <div>
        <div v-if="props.recipe?.creatorId === AppState.account?.sub" class="recipeFavoriteCreator d-flex align-items-center justify-content-center flex-column px-1 m-2 z-0 position-relative">
            <i v-if="!props.recipe.favorite" @click="favorite()" class="mdi mdi-bookmark-outline fs-4 d-block" style="cursor:pointer; color:white;"></i>
            <i v-if="props.recipe.favorite" @click="unfavorite()" class="mdi mdi-bookmark fs-4 d-block" style="cursor:pointer; color:gold;"></i>
            <i @click="deleteRecipe()" class="mdi mdi-trash-can-outline fs-4 d-block text-danger" style="cursor:pointer;"></i>
        </div>
        <div v-if="props.recipe?.creatorId !== AppState.account?.sub" class="recipeFavorite d-flex justify-content-center px-1 m-2 z-0 position-relative">
            <i v-if="!props.recipe.favorite" @click="favorite()" class="mdi mdi-bookmark-outline fs-4 d-block" style="cursor:pointer; color:white;"></i>
            <i v-if="props.recipe.favorite" @click="unfavorite()" class="mdi mdi-bookmark fs-4 d-block" style="cursor:pointer; color:gold;"></i>
        </div>
        <div class="recipeCardContainer d-flex flex-column justify-content-between z-1 position-relative bg-white" :style="{ backgroundImage: `url(${props.recipe.img})` }" data-bs-toggle="modal" data-bs-target="#recipeModal">
            <div class="d-flex justify-content-between">
                <div class="recipeCategory text-light w-auto d-inline-block rounded-pill m-2">
                    <p class="m-0 fw-semibold px-2">{{ convertToCategory(props.recipe.category) }}</p>
                </div>
            </div>
            <div class="m-2">
                <p class="recipeTitle m-0 fw-semibold px-2 text-light fs-3">{{ props.recipe.title }}</p>
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped>
    .recipeCardContainer{
        transition:.2s;
        aspect-ratio:1;
        border-radius:3px;
        background-size:cover;
        box-shadow:0px 0px 20px rgb(205, 205, 205);
        background-position:center;
    }

    .recipeCardContainer:hover{
        cursor:pointer;
        transform:scale(1.01);
        filter:brightness(0.9);
    }

    .recipeCategory{
        max-width:fit-content;
    }

    .recipeCategory, .recipeTitle{
        background-color: rgba(0, 0, 0, 0.3);
        padding: 8px;
        border-radius: 3px;
        backdrop-filter: blur(5px);
    }

    .recipeFavorite{
        transition:.2s;
        background-color: rgb(74, 74, 74);
        border-radius: 3px;
        height:72px;
        width:36px;
        transform:translateY(90%);
    }

    .recipeFavorite:hover{
        transform:translateY(60%);
    }

    .recipeFavoriteCreator{
        transition:.2s;
        background-color: rgb(74, 74, 74);
        border-radius: 3px;
        height:72px;
        width:36px;
        transform:translateY(90%);
    }

    .recipeFavoriteCreator:hover{
        transform:translateY(15%);
    }
</style>