import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import login from '@/views/LoginView.vue'
import Product_detail from '@/views/ProductDetailView.vue'
import product_by_category from '@/views/Product_By_CategoryView.vue'


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/Login',
      name: 'Login',
      component: login
    },
    {
      path: '/product/:categoryId?',
      name: 'product_by_category',
      component: product_by_category
    },
    {
      path: '/Product_detail/:Id?', 
      name: 'Product_detail/:Id',
      component: Product_detail
    },
  ]
})

export default router
