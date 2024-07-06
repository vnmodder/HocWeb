import { createRouter, createWebHistory } from "vue-router";
import Layout from "@/layout/AdminLayout.vue";
import LoginView from "@/views/LoginView.vue";
import DashboardView from "@/views/DashboardView.vue";
import NotFound from "@/views/NotFound.vue";
import CategoryView from "@/views/CategoryView.vue";

const routeItem = {
  path: "",
  component: Layout,
  children: [
    {
      path: "",
      name: "dashboard",
      component: DashboardView,
    },
    {
      path: "category",
      name: "category",
      component: CategoryView,
    },
  ],
};

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { ...routeItem },
    {
      path: "/login",
      name: "login",
      component: LoginView,
    },
    {
      path: "/:pathMatch(.*)*",
      name: "not-found",
      component: NotFound,
    },
  ],
});

export default router;