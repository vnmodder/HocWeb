import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import About from '@/views/AboutView.vue'
import LoginView from '@/views/LoginView.vue'
import ProductView from '@/views/ProductView.vue'
import ProductDetail from '@/views/ProductDetail.vue'
import ContactView from '@/views/ContactView.vue'
import CartView from '@/views/CartView.vue'
import OrderView from '@/views/OrderView.vue'
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
      component: About
    },
    {
      path: '/contact',
      name: 'contact',
      component: ContactView
    },
    {
      path: '/cart',
      name: 'cart',
      component: CartView
    },
    {
      path: '/order',
      name: 'order',
      component: OrderView
    },
    {
      path: '/product/:categoryId?',
      name: 'product',
      component: ProductView
    },
    {
      path: '/product-detail/:id?',
      name: 'product-detail',
      component: ProductDetail
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/register',
      name: 'register',
      component: RegisterView
    },
  ]
})

export default router
