<script setup>
import { recipeService } from '@/services/RecipeService';
import Login from './Login.vue';
import { logger } from '@/utils/Logger';

    function getAll() {
        try{
            recipeService.getRecipes();
        } catch(err) {
            logger.error("Could not get recipes",err);
        }
    }

    function getMyRecipes() {
        try{
            recipeService.getUserRecipes();
        } catch(err) {
            logger.error("Could not get user recipes",err);
        }
    }

    function getFavorites() {
        try{
            recipeService.getFavoriteRecipes();
        } catch(err) {
            logger.error("Could not get favorite recipes",err);
        }
    }

</script>

<template>
    <div class="bannerContainer mt-4 mb-5 text-sahitya">
        <div class="w-100 clearfix">
            <div class="d-flex m-3 gap-4 float-end" style="width:25%;">
                <input type="text" class="searchField form-control float-end" placeholder="Search...">
                <Login />
            </div>
        </div>
        <div class="text-border-transparent text-light text-center">
            <h1 style="font-size:48px;">All-Spice</h1>
            <h4 class="text-border-transparent lh-base">Cherish Your Family<br>And Their Cooking</h4>
        </div>
        <div class="bannerLinks bg-light m-auto text-center fs-4 d-flex justify-content-between px-5 align-items-center" style="height:60px; width:40%;">
            <a @click="getAll()" class="text-primary">Home</a>
            <a @click="getMyRecipes()" class="text-primary">My Recipes</a>
            <a @click="getFavorites()" class="text-primary">Favorites</a>
        </div>
    </div>
</template>

<style lang="scss" scoped>
    .bannerContainer{
        height:300px;
        background-image:url(https://images.unsplash.com/photo-1550599112-0c21a831f6b9);
        background-size: cover;
        border-radius:5px;
        box-shadow:0px 0px 50px rgb(132, 132, 132);
    }

    .searchField{
        background-image:url("../assets/img/search.png");
        background-size:8%;
        background-repeat:no-repeat;
        background-position-x:95%;
        background-position-y:center;
        border-radius:3px;
    }

    .text-border-transparent{
        text-shadow: -1px 0 rgba(0, 0, 0, 0.2), 1px 0 rgba(0, 0, 0, 0.2), 0 -1px rgba(0, 0, 0, 0.2), 0 1px rgba(0, 0, 0, 0.2);
    }

    .bannerLinks{
        border-radius:5px;
        box-shadow:0px 0px 50px rgb(132, 132, 132);
        transform:translateY(90%);
        a{
            cursor:pointer;
            text-decoration:none;
        }
        a:hover{
            text-decoration:underline;
        }
    }
</style>