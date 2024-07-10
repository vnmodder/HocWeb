<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <div class="container-fluid">
      <a class="navbar-brand" href="/">Web</a>
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li class="nav-item">
            <a class="nav-link" href="#">Link</a>
          </li>
          <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
              Dropdown
            </a>
            <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
              <li v-for="item in categories" :key="item.id">
                <a class="dropdown-item" href="/product?categoryId=" + item.id>{{ item.nameVN }}</a>
              </li>
            </ul>
          </li>
          <li class="nav-item">
            <a class="nav-link disabled" href="#" tabindex="-1" aria-disabled="true">Disabled</a>
          </li>
        </ul>
        <form class="d-flex me-3">
          <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
          <button class="btn btn-outline-success" type="submit">Search</button>
        </form>
        <div class="d-flex align-items-center">
          <div class="position-relative me-3">
            <img src="https://cdn-icons-png.flaticon.com/512/1170/1170678.png" alt="Cart Icon" class="img-fluid" style="width: 30px;">
            <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
              0
            </span>
          </div>
          <a href="/login"><button class="btn btn-primary">Đăng Nhập</button></a>
        </div>
      </div>
    </div>
  </nav>
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

    const getAllCategory = async ()=> {
        return await apiClient.get(`Category/get-all`, {
        headers : { Authorization : `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJhNDdhMWVmMC0wZGVmLTQzMDQtYTA4MS00Mjg0YzA2MWNkMGYiLCJpYXQiOjE3MjA2MjAxOTIsInJvbGUiOiJVc2VyLEFkbWluIiwibmFtZWlkIjoiYWRtaW4iLCJlbWFpbCI6ImFudGVzdG1haWxAZ21haWwuY29tIiwidW5pcXVlX25hbWUiOiIxIiwibmJmIjoxNzIwNjIwMTkyLCJleHAiOjE3MjEyNTAxOTIsImlzcyI6IkhvY1dlYklzc3VlciIsImF1ZCI6IkhvY1dlYkF1ZGllbmNlIn0._EyIyuLou-YWFBnRoW-8rDmiOPIeBTtHQqZhZKYFX7E` },    
        });
    } 
    const test =async()=>{
        const result = await getAllCategory();
        if(result.status == 200){
            console.log(result.data.result.data)
            categories.value = result.data.result.data
        }
    }

    const categories = ref<Array<any>>([])
      test();

</script>