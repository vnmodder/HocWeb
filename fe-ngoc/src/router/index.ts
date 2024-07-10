import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import login from '@/views/LoginView.vue'
import Home from '@/views/HomeView.vue'
import detail from '@/views/ProductDetailView.vue'
import product from '@/views/ProductView.vue'

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
      path: '/Detail/:ID?',
      name: 'Detail',
      component: detail
    },
    {
      path: '/Product/:categoryID?',
      name: 'product',
      component: product
    },
  ]
})

export default router
