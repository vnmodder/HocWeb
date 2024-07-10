<template>
<div class="container mt-4">
        <div class="row">
            <div v-for="item in product" :key="item.id" class="col-3">
                <div class="card">
                    <img :src="item.image" class="card-img-top" :alt="`Product ${item.name}`">
                    <div class="card-body">
                        <h5 class="card-title">{{ item.name }}</h5>
                        <p class="card-text">{{ item.description }}</p>
                        <div class="d-flex justify-content-between">
                            <a href="/Detail?ID=" + item.id class="btn btn-primary">View</a>
                            <a href="#" class="btn btn-secondary">Add to Cart</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>

<script setup lang='ts'>
import axios from 'axios';
import { ref } from 'vue';
    const apiClient = axios.create({
        baseURL: 'http://localhost:5152/api/',
        headers: {
            'Content-Type' : 'application/json',
        },
    });

    const getAllProduct = async ()=> {
        return await apiClient.get(`Product/get-all`, {
        headers : { Authorization : `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI3MTRkNjdlYy02MzhiLTQxN2UtYjFlMy0xZTQyZTBmOGNhZmYiLCJpYXQiOjE3MTk4MTU3MDksInJvbGUiOiJVc2VyIiwibmFtZWlkIjoiYWRtaW4iLCJlbWFpbCI6ImFudGVzdG1haWxAZ21haWwuY29tIiwidW5pcXVlX25hbWUiOiIxIiwibmJmIjoxNzE5ODE1NzA5LCJleHAiOjE3MjA0NDU3MDksImlzcyI6IkhvY1dlYklzc3VlciIsImF1ZCI6IkhvY1dlYkF1ZGllbmNlIn0.3C7Kl-EfbCn7Uurh2_nsbnqUP62qE-KuVjdHDAnmW7Q.kE7LwXMtLlG4r4JCYsem2lATyRH3sju5lXOce60WcNY` },    
        });
    } 
    const test =async()=>{
        const result = await getAllProduct();
        if(result.status == 200){
            console.log(result.data.result.data)
            product.value = result.data.result.data
        }
    }

    const product = ref<Array<any>>([])

test();
</script>