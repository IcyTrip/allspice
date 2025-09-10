<script setup>
import { AppState } from '@/AppState';
import { computed } from 'vue';

    const recipe = computed(() => AppState.activeRecipe);
    const ingredients = computed(() => AppState.ingredients.filter(i => i.recipeId === recipe.value?.id));

    function setDeleteRecipe() {
        AppState.deleteRecipe = recipe;
    }
</script>

<template>
    <div class="modal fade modal-xl" id="recipeModal" tab-index="-1" role="dialog" aria-labelledby="recipeModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body m-0 p-0 d-flex">
                    <div class="w-50">
                        <img :src="recipe?.img" class="w-100">
                    </div>
                    <div class="w-50 p-4">
                        <div class="d-flex align-items-center justify-content-between">
                            <div class="d-flex align-items-center">
                                <h2 class="text-primary m-0">{{ recipe?.title }}</h2>
                                <button type="button" class="btn btn-primary dropdown-toggle px-3 py-1 mx-4" id="dropdownButton" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="mdi mdi-pencil fs-5"></i></button>
                                <div class="dropdown-menu py-0" aria-labelledby="dropdownButton">
                                    <a class="dropdown-item py-0 border-bottom">Edit Recipe</a>
                                    <a @click="setDeleteRecipe()" class="dropdown-item py-0 text-danger" data-bs-toggle="modal" data-bs-target="#deleteModal">Delete Recipe</a>
                                </div>
                            </div>
                            <button type="button" class="btn btn-close btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <ul>
                            <li v-for="i in ingredients" :key="i.id">
                                {{ i.quantity }} {{ i.name }}
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped>
    .dropdown-item{
        cursor:pointer;
        user-select:none;
    }
</style>