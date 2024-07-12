<template>
<div class="container mt-6 mb-6">
    <div class="row">
        <div class="col-lg-4 col-md-6 mb-4" v-for="item in products" :key="item.id">
            <div class="card h-100">
                <img :src="item.image" class="card-img-top" :alt="`Product ${item.name}`">
                <div class="card-body">
                    <h5 class="card-title">{{ item.name }}</h5>
                    <p class="card-text">{{ item.description }}</p>
                </div>
                <div class="card-footer d-flex justify-content-between">
                    <a :href="'/Product_detail?Id=' + item.id"class="btn btn-primary">View</a>
                    <a href="#" class="btn btn-secondary">Add to Cart</a>
                </div>
            </div>
        </div>
    </div>
    <br><br>
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