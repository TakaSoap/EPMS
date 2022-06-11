import { createApp } from "vue";
import App from "./App.vue";
import createSiteRouter from "./router/router";
import routes from '~pages'
import '@/style.css'

const app = createApp(App);
const router = createSiteRouter(routes);
app.use(router);

router.isReady().then(() => {
  app.mount("#app");
});
