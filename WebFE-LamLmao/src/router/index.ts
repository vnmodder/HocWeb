import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import AboutView from '@/views/AboutView.vue'
import LoginView from '@/views/LoginView.vue'
import ProductDetail from '@/views/ProductDetail.vue'
import CartView from '@/views/CartView.vue'
import ProductView from '@/views/ProductView.vue'
import CheckOutView from '@/views/CheckOutView.vue'
import RegisterView from '@/views/RegisterView.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/about', 
      name: 'about',
      component: AboutView
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path : '/product-detail/:id?',
      name: 'product-detail',
      component: ProductDetail
    },
    {
      path: '/cart',
      name: 'cart',
      component : CartView
    },
    {
      path : '/product/:categoryId?',
      name : 'product',
      component : ProductView
    },
    {
      path : '/checkout',
      name : 'checkout',
      component : CheckOutView
    },
    {
      path : '/register',
      name : 'register',
      component : RegisterView
    }
  ]
})

export default router
