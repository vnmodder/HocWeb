<template>
  <!-- Banner -->
  <div class="banner">
    <div class="container">
      <div class="row">
        <div class="col-lg-12">
          <h1 class="banner-title">Banner</h1>
          <p class="banner-text">Tieu de</p>
        </div>
      </div>
    </div>
  </div>
  <section>
    <h2 style="margin-top: 20px" class="fw-bolder mb-4">DANH MỤC</h2>
    <!-- Category -->
    <div class="d-flex justify-content-around">
      <div v-for="item in items" style="margin-top: 0px" class="items">
        <div class="">
          <a :href="'/product?categoryid=' + item.id"
            ><img :src="item.image" class="item-image" alt="..."
          /></a>
          <div class="card-body">
            <p class="card-text" style="text-align: center">
              {{ item.name }}
            </p>
          </div>
        </div>
      </div>
    </div>
  </section>
  <section>
    <h2 style="margin-top: 50px" class="fw-bolder mb-4">
      CÁC SẢN PHẨM
    </h2>
    <div class="container px-4 px-lg-5 mt-2">
      <div
        class="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center"
      >
        <div
          v-for="item in itemsProduct"
          
          class="col mb-5"
        >
          <div class="card h-100">
            <a :href="'/product-detail?id=' + item.id">
              <img class="card-img-top" :src="item.image" alt="..." />
            </a>

            <div class="card-body p-4">
              <div class="text-center">
                <h5 class="fw-bolder">{{ item.name }}</h5>
                <span
                  >{{
                    new Intl.NumberFormat("vi-vn").format(item.unitPrice)
                  }}đ</span
                >
              </div>
            </div>
            <div
              class="card-footer p-4 pt-0 border-top-0 bg-transparent d-flex justify-content-center"
            >
              <div class="text-center">
                <a
                  class="btn btn-outline-dark mt-auto"
                  :href="'/product-detail?id=' + item.id"
                  >Xem</a
                >
              </div>
              <div class="text-center">
                <button
                  class="btn btn-outline-dark mt-auto ms-2"
                  @click="addToCart(item)"
                >
                  Thêm vào giỏ
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
<script setup lang="ts">
import homeApi from "@/api/home.api";
import { ref } from "vue";
import { useCartStore } from "@/stores/cart";
import { storeToRefs } from "pinia";
import { userStore } from "../stores/auth";
import { useRoute } from "vue-router";

const authStore = userStore();
const { user } = storeToRefs(authStore);
const route = useRoute();

const addToCart = (item: any) => {
  if (!user.value) {
    alert("Hãy đăng nhập để thực hiện chức năng này");
    return;
  }

  

  const product = {
    id: item.id,
    name: item.name,
    unitPrice: item.unitPrice,
    quantity: 1,
  };
  useCartStore().addToCart(product);
};
const items = ref<any>([]);
const itemsProduct = ref<any>([]);

const fetchData = async () => {
  const response = await homeApi.getAllCategory();
  if (response && response.data.result.isSuccess) {
    items.value = response.data.result.data;
  } else {
    items.value = [];
    alert("Lỗi");
  }

  const response2 = await homeApi.getAllProduct();
  if (response2 && response2.data.result.isSuccess) {
    itemsProduct.value = response2.data.result.data;
  } else {
    itemsProduct.value = [];
    alert(response2?.data.result.message ?? "Lỗi");
  }
};


fetchData();
</script>

<script lang="ts">
export default {
  name: "Banner",
};
</script>

<style scoped>
.banner {
  background-color: #b8c3db;
  padding: 50px 0;
  text-align: center;
}

.items {
  transition: transform 0.5s;
}

.items:hover {
  transform: scale(1.1);
}

.banner-title {
  font-size: 2.5em;
  color: #333;
  margin-bottom: 10px;
}

.banner-text {
  font-size: 1.2em;
  color: #666;
}

.item-image {
  width: 200px;
  height: 200px;
}

.card-img-top {
  height: 200px;
  object-fit: cover;
  width: 100%;
}
</style>
