<template>
<div class="container mt-4">
        <div class="row">
            <div v-for="item in products" :key="item.id" class="col-3">
                <div class="card">
                    <img :src="item.image" class="card-img-top" :alt="`Product ${item.name}`">
                    <div class="card-body">
                        <h5 class="card-title">{{ item.name }}</h5>
                        <p class="card-text">{{ item.description }}</p>
                        <div class="d-flex justify-content-between">
                            <a class="btn btn-primary">View</a>
                            <a href="#" class="btn btn-secondary">Add to Cart</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>

<script setup lang='ts'>
import { ref } from 'vue';
import homeApi from '@/api/home.api';

const products = ref<any>([]);

const fetchData = async () => {
    const response = await homeApi.getAllProduct();
    console.log(response);
    if(response && response.data.result.isSuccess){
        products.value = response.data.result.data; 
    }
    else{
        alert(response?.data.result.message ?? "Lỗi");
    }
};
fetchData();
</script>