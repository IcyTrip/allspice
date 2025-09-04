<script setup>
import Login from './Login.vue';


</script>

<template>
    <div class="bannerContainer my-4">
        <div class="d-flex m-3 gap-4" style="width:25%; float:right;">
            <input type="text" class="searchField form-control float-end" placeholder="Search...">
            <Login />
        </div>
        <div class="text-light">
            <h1>AllSpice</h1>
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
</style>