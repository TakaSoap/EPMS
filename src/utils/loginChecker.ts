import { authenticationClient } from "./authing";
import { useMessage } from "naive-ui";

const message = useMessage();
const router = useRouter();

export function checkLogin() {
  authenticationClient
    .getCurrentUser()
    .then((user) => {
      console.log(user);
      if (user) {
        console.log("user logged in");
        return true;
      } else {
        console.log("user not logged in");
        message.error("请先登录");
        router.push("/login");
        return false;
      }
    })
    .catch((err) => {
      console.log(err);
      message.error("请先登录");
      router.push("/login");
      return false;
    });
}
