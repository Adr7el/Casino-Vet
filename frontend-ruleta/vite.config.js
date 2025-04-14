import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import path from 'path'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  define: {
    'global.crypto': 'crypto',
  },
  resolve: {
    alias: {
      '@': path.resolve(__dirname, 'src'),
      crypto: 'crypto-browserify',
    },
  },
  // server: {
  //   port: 3000, // Cambiar el puerto a 3000 o el que prefieras
  // },
});
