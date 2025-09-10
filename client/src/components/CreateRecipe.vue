<script setup>
import { ingredientService } from '@/services/IngredientService';
import { recipeService } from '@/services/RecipeService';
import { logger } from '@/utils/Logger';
import { onMounted, ref } from 'vue';

    const title = ref('');
    const category = ref('');
    const img = ref('');
    const instructions = ref('');
    const ingredientQuantity = ref('');
    const ingredientName = ref('');

    const tempIngredients = ref([]);
    const tempQuant = ref([]);
    const tempName = ref([]);

    const isUrlValid = ref(false);

    async function createRecipe() {
        try{
            const newRecipe = {
                title: title.value,
                category: Number(category.value),
                img: img.value,
                instructions: instructions.value
            };
            const createdRecipe = await recipeService.createRecipe(newRecipe);
            await createIngredients(createdRecipe.id);
        } catch(err) {
            logger.error("Could not create blog", err);
        }
    }

    function addIngredient() {
        tempIngredients.value.push(ingredientQuantity.value + " " + ingredientName.value);
        tempQuant.value.push(ingredientQuantity.value);
        tempName.value.push(ingredientName.value);

        ingredientQuantity.value = '';
        ingredientName.value = '';
    }

    async function createIngredients(recipeId) {
        try{
            for(let i = 0; i < tempIngredients.value.length; i++) {
                const newIngredient = {
                    name: tempName.value[i],
                    quantity: tempQuant.value[i],
                    recipeId: recipeId
                }
                ingredientService.createIngredient(newIngredient);
            }
            tempIngredients.value = [];
            tempQuant.value = [];
            tempName.value = [];
            title.value = '';
            category.value = '0';
            img.value = '';
            instructions.value = '';
            ingredientQuantity.value = '';
            ingredientName.value = '';

        } catch(err) {
            logger.error("Could not create ingredients",err);
        }
    }
    
    function checkUrl() {
        isUrlValid.value = recipeService.isValidUrl(img.value);
    }

    onMounted(() => {
        category.value = "0";
    })
</script>

<template>
    <div>
        <button class="createButton btn btn-primary rounded-circle position-fixed bottom-0 end-0 m-5 d-flex align-items-center justify-content-center p-0" data-bs-toggle="modal" data-bs-target="#createRecipeModal"><i class="mdi mdi-plus"></i></button>
        <div>
            <div class="modal fade modal-lg" id="createRecipeModal" tab-index="-1" role="dialog" aria-labelledby="createRecipeModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header border-0 bg-primary">
                            <h4 class="modal-title fw-bold text-light" id="createRecipeModalLabel">New Recipe</h4>
                            <button type="button" class="btn btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body mx-0 mx-md-5">
                            <form class="row">
                                <div class="form-group mb-3 col-6">
                                    <label for="title">Title</label>
                                    <input type="text" class="form-control" id="title" placeholder="Title..." v-model="title">
                                </div>
                                <div class="form-group mb-3 col-6">
                                    <label for="category">Category</label>
                                    <select id="category" class="form-select" aria-label="Category select" v-model="category">
                                        <option selected value="0">Select Category...</option>
                                        <option value="1">Breakfast</option>
                                        <option value="2">Lunch</option>
                                        <option value="3">Dinner</option>
                                        <option value="4">Snack</option>
                                        <option value="5">Dessert</option>
                                    </select>
                                </div>
                                <div class="form-group mb-3">
                                    <label for="image">Image</label>
                                    <input @keyup="checkUrl()" type="text" class="form-control" id="image" placeholder='Image Url...' v-model="img">
                                    <p v-if="!isUrlValid" class="text-danger p-0 m-0" style="font-size:.8rem;">Link empty or not valid.</p>
                                </div>
                                <div class="form-group mb-3">
                                    <label>Ingredients</label>
                                    <div class="d-flex gap-5">
                                        <input type="text" class="form-control" placeholder="Quantity..." style="width:40%;" v-model="ingredientQuantity">
                                        <input type="text" class="form-control" placeholder="Ingredient Name..." v-model="ingredientName">
                                        <button @click="addIngredient()" type="button" class="btn btn-primary rounded-circle" style="aspect-ratio:1/1;"><i class="mdi mdi-plus"></i></button>
                                    </div>
                                    <ul>
                                        <li v-for="i in tempIngredients" :key="i" class="mb-1">{{ i }}</li>
                                    </ul>
                                </div>
                                <div class="form-group mb-3">
                                    <label for="instructions">Instructions</label>
                                    <textarea type="text" class="form-control" id="instructions" placeholder="Instructions..." style="min-height:200px;" v-model="instructions"></textarea>
                                </div>
                                <div class="form-group mb-1 d-flex gap-2">
                                    <button :disabled="category === '0' || !isUrlValid || title == '' || instructions == ''" @click="createRecipe()" type="button" class="btn btn-primary" data-bs-dismiss="modal">Create Recipe</button>
                                    <button type="button" class="cancelButton btn" data-bs-dismiss="modal">Cancel</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped>
    .createButton{
        aspect-ratio:1;
        width:75px;
        font-size:3rem;
    }

    .cancelButton:hover{
        text-decoration: underline;
    }
</style>