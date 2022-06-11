import { createRouter, createWebHistory } from "vue-router";

import { nextTick } from "vue";

export const loadingBarApiRef: any = {};

export default function createSiteRouter(routes: any) {
  const router = createRouter({
    history: createWebHistory(),
    routes,
  });

  router.beforeEach(function (to, from, next) {
    if (!from || to.path !== from.path) {
      if (loadingBarApiRef.value) {
        loadingBarApiRef.value.start();
      }
    }
    next();
  });

  router.afterEach(function (to, from) {
    if (!from || to.path !== from.path) {
      if (loadingBarApiRef.value) {
        loadingBarApiRef.value.finish();
      }
      if (to.hash && to.hash !== from.hash) {
        nextTick(() => {
          const el = document.querySelector(to.hash);
          if (el) el.scrollIntoView();
        });
      }
      nextTick(() => {
        const h1s = document.getElementsByTagName("h1");
        if (to.name !== "home" && h1s.length !== 0) {
          document.title = h1s[0].textContent + " - EPMS";
        }
        // else, defined in index.html
      });
    }
  });

  return router;
}
