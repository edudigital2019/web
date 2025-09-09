import { bootstrapApplication } from '@angular/platform-browser';
import { App } from './app/app';
import { config } from './app/app.config.server';
import {
  provideServerRendering,
  withRoutes,
  RenderMode,
  type ServerRoute,
} from '@angular/ssr';

// Rutas del lado servidor:
// - Prerender para las estáticas
// - SSR para la ruta con parámetro (editar) y comodín
const serverRoutes: ServerRoute[] = [
  { path: '', renderMode: RenderMode.Prerender },            // "/"
  { path: 'personas', renderMode: RenderMode.Prerender },
  { path: 'usuarios', renderMode: RenderMode.Prerender },
  { path: 'nosotros', renderMode: RenderMode.Prerender },
  { path: 'personas/crear', renderMode: RenderMode.Prerender },

  { path: 'personas/editar/:id', renderMode: RenderMode.Server }, // <-- sin prerender

  { path: '**', renderMode: RenderMode.Server }, // fallback SSR para cualquier otra
];

const bootstrap = () =>
  bootstrapApplication(App, {
    ...config,
    providers: [
      ...(config.providers ?? []),
      provideServerRendering(withRoutes(serverRoutes)),
    ],
  });

export default bootstrap;
