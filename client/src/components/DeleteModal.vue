<script setup>
import { AppState } from '@/AppState';
import { recipeService } from '@/services/RecipeService';
import { logger } from '@/utils/Logger';
import { computed } from 'vue';

    const recipeToDelete = computed(() => AppState.deleteRecipe);

    function deleteRecipe() {
        try{
            recipeService.deleteRecipe(recipeToDelete.value.id);
        } catch(err) {
            logger.error("Could not delete recipe",err);
        }
    }
</script>

<template>
    <div class="modal fade modal-md" id="deleteModal" tab-index="-1" role="dialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title fw-bold" id="deleteModalLabel">Delete Confirmation</h4>
                    <button type="button" class="btn btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body m-3 text-center">
                    <h5>Are you sure you want to delete this? This action cannot be undone.</h5>
                </div>
                <div class="modal-footer d-flex justify-content-between">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" aria-label="Close">Cancel</button>
                    <button @click="deleteRecipe()" type="button" class="btn btn-danger" data-bs-dismiss="modal" aria-label="Close">Delete</button>
                </div>
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped>

</style>