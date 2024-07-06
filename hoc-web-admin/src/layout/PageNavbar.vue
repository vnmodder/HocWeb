<template>
  <nav class="navbar navbar-expand px-3 border-bottom">
    <button class="btn" id="sidebar-toggle" type="button">
      <span class="navbar-toggler-icon" @click="menuToggleClick"></span>
    </button>
    <div class="navbar-collapse navbar">
      <ul class="navbar-nav">
        <li class="nav-item dropdown">
          <a href="#" data-bs-toggle="dropdown" class="nav-icon pe-md-0">
            <img src="@/assets/hw.png" class="avatar" alt="" />
          </a>
          <div class="dropdown-menu dropdown-menu-end">
            <a href="#" class="dropdown-item">Đăng xuất</a>
          </div>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script setup lang="ts">
import { defineProps } from "vue";

interface Props {
  menuToggleClick?: () => void;
}

withDefaults(defineProps<Props>(), {
  menuToggleClick: Function,
});
</script>
