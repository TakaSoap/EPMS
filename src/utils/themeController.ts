import { darkTheme, useOsTheme } from "naive-ui";
import { BuiltInGlobalTheme } from "naive-ui/lib/themes/interface";

const osThemeRef = useOsTheme()

export const state: ThemeState = reactive({
  theme: osThemeRef.value === 'dark' ? darkTheme : null,
  isDarkTheme: osThemeRef.value === 'dark' ? true : false,
});

interface ThemeState {
  theme: null | BuiltInGlobalTheme;
  isDarkTheme: boolean;
}

export function toggleTheme() {
  state.isDarkTheme = !state.isDarkTheme;
  if (state.isDarkTheme) {
    state.theme = darkTheme;
  } else {
    state.theme = null;
  }
}