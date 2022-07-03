import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import Components from "unplugin-vue-components/vite";
import { NaiveUiResolver } from "unplugin-vue-components/resolvers";
import Pages from "vite-plugin-pages";
import AutoImport from "unplugin-auto-import/vite";
import { fileURLToPath, URL } from "url";
import { esbuildCommonjs, viteCommonjs } from "@originjs/vite-plugin-commonjs";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    Components({
      resolvers: [NaiveUiResolver()],
    }),
    Pages({
      extendRoute(route, parent) {
        if (route.path === "/") {
          // Index is unauthenticated.
          return {
            ...route,
            redirect: "/welcome",
          };
        }

        // Augment the route with meta that indicates that the route requires authentication.
        //TODO: add auth check

        // return {
        //   ...route,
        //   meta: { auth: true },
        // };
      },
    }),
    AutoImport({
      imports: ["vue", "vue-router"],
    }),
    // viteCommonjs({
    //   include: ["node_modules/authing-js-sdk"],
    // }),
  ],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", import.meta.url)),
    },
  },
  build: {
    commonjsOptions: {
    },
  },
});
