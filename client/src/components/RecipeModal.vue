<script setup>
import { AppState } from '@/AppState';
import { ingredientService } from '@/services/IngredientService';
import { recipeService } from '@/services/RecipeService';
import { logger } from '@/utils/Logger';
import { computed, ref } from 'vue';

    const recipe = computed(() => AppState.activeRecipe);
    const ingredients = computed(() => AppState.ingredients.filter(i => i.recipeId === recipe.value?.id));
    const editing = ref(false);
    const tempInstructions = ref('');
    const tempIngredients = ref([]);
    const del = ref(null);
    const ingredientQuantity = ref('');
    const ingredientName = ref('');
    const addedIngredients = ref([]);

    function setDeleteRecipe() {
        AppState.deleteRecipe = recipe;
    }

    function editRecipe() {
        try{
            addedIngredients.value = [];
            recipeService.updateRecipe(recipe.value.id, recipe.value.instructions);
            if(tempIngredients.value.length != 0) {
                for(let i = 0; i < tempIngredients.value.length; i++) {
                    ingredientService.deleteIngredient(tempIngredients.value[i]);
                }
            }
            editing.value = false;
        } catch(err) {
            logger.error("Could not edit recipe",err);
        }
    }

    function deleteIngredient(id) {
        try{
            if(tempIngredients.value.includes(id)) {
                tempIngredients.value = tempIngredients.value.filter(i => i.id === id);
            }
            else{
                tempIngredients.value.push(id);
            }
        } catch(err) {
            logger.error("Could not delete recipe",err);
        }
    }

    function beginEditing() {
        tempInstructions.value = recipe.value.instructions;
        editing.value = true;
    }

    function cancelEditing() {
        recipe.value.instructions = tempInstructions.value;
        tempInstructions.value = '';
        tempIngredients.value = [];
        editing.value = false;
        try{
            addedIngredients.value.forEach(i => {
                ingredientService.deleteIngredient(i.id);
            });
        } catch(err) {
            logger.error("Could not delete ingredient",err);
        }
    }

    async function addIngredient() {
        try{
            const newIngredient = {
                name: ingredientName.value,
                quantity: ingredientQuantity.value,
                recipeId: recipe.value.id
            }
            const createdIngredient = await ingredientService.createIngredient(newIngredient);
            addedIngredients.value.push(createdIngredient);
            ingredientQuantity.value = '';
            ingredientName.value = '';
        } catch(err) {
            logger.error("Could not add ingredient",err);
        }
    }

</script>

<template>
    <div class="modal fade modal-xl" id="recipeModal" tab-index="-1" role="dialog" aria-labelledby="recipeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-body m-0 p-0 d-flex">
                    <div class="w-50 d-flex">
                        <img :src="recipe?.img" class="w-100 object-fit-cover">
                    </div>
                    <div class="w-50 p-4">
                        <div class="d-flex align-items-center justify-content-between">
                            <div class="d-flex align-items-center">
                                <h2 class="text-primary m-0">{{ recipe?.title }}</h2>
                                <button v-if="recipe?.creatorId === AppState.account?.sub" type="button" class="btn btn-primary dropdown-toggle px-3 py-1 mx-4" id="dropdownButton" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="mdi mdi-pencil fs-5"></i></button>
                                <div class="dropdown-menu py-0" aria-labelledby="dropdownButton">
                                    <a @click="beginEditing()" class="dropdown-item py-0 border-bottom">Edit Recipe</a>
                                    <a @click="setDeleteRecipe()" class="dropdown-item py-0 text-danger" data-bs-toggle="modal" data-bs-target="#deleteModal">Delete Recipe</a>
                                </div>
                            </div>
                            <button @click="cancelEditing();" type="button" class="btn btn-close btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <p class="text-secondary">By: {{ recipe?.creator.name }}</p>
                        <p class="bg-secondary text-light w-auto d-inline-block rounded-pill px-2 fw-semibold">{{ recipe?.category }}</p>
                        <h3>Ingredients</h3>
                        <ul :style="[editing ? { 'padding-left':'0' } : {}]">
                            <li v-for="i in ingredients.sort((a,b) => a.id - b.id)" :key="i.id" :style="[editing ? { 'list-style-type':'none' } : {}, tempIngredients.includes(i.id) ? { 'text-decoration':'line-through' } : {}]">
                                <i @click="deleteIngredient(i.id)" @mouseover="del = i.id" @mouseleave="del = null" v-if="editing" :class="del === i.id ? 'mdi mdi-delete-empty text-danger' : 'mdi mdi-delete text-danger'" style="padding-right:16px"></i>
                                {{ i.quantity }} {{ i.name }}
                            </li>
                        </ul>
                        <form v-if="editing">
                            <div class="form-group d-flex gap-5">
                                <input type="text" class="form-control w-50" placeholder="Quantity..." v-model="ingredientQuantity">
                                <input type="text" class="form-control" placeholder="Name..." v-model="ingredientName">
                                <button @click="addIngredient()" type="button" class="btn btn-primary rounded-circle" style="aspect-ratio:1/1;"><i class="mdi mdi-plus"></i></button>
                            </div>
                        </form>
                        <h3>Instructions</h3>
                        <p v-if="!editing" style="white-space:pre-line;">{{ recipe?.instructions }}</p>
                        <div v-if="editing">
                            <textarea class="form-control border-2" placeholder="Instructions..." v-model="recipe.instructions" style="min-height:150px;"></textarea>
                            <button @click="editRecipe()" type="button" class="btn btn-primary mt-3 mx-1">Save Changes</button>
                            <button @click="cancelEditing()" type="button" class="btn btn-secondary mt-3 mx-1">Cancel Editing</button>
                        </div>
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

    .mdi-delete, .mdi-delete-empty{
        transition:.2s;
        cursor:pointer;
    }

    .mdi-delete:hover, .mdi-delete-empty:hover{
        filter:brightness(0.6);
    }
</style>