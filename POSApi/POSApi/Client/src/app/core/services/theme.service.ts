import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ThemeService {
  private themeLinkId = 'app-theme';
  private themePath = 'primeng-themes';

  constructor() {
    const savedTheme = localStorage.getItem('theme') || 'aura-light';
    this.loadTheme(savedTheme);
  }

  loadTheme(theme: string) {
    const head = document.getElementsByTagName('head')[0];
    let themeLink = document.getElementById(this.themeLinkId) as HTMLLinkElement;

    const href = `${this.themePath}/${theme}/theme.css`;

    console.log('Loading theme:', href);  // Provjera

    if (themeLink) {
      themeLink.href = href;
    } else {
      const linkEl = document.createElement('link');
      linkEl.rel = 'stylesheet';
      linkEl.id = this.themeLinkId;
      linkEl.href = href;
      head.appendChild(linkEl);
    }

    localStorage.setItem('theme', theme);
  }

  switchTheme(): string {
    const current = localStorage.getItem('theme') || 'aura-light';
    const next = current === 'aura-light' ? 'aura-dark' : 'aura-light';
    this.loadTheme(next);
    return next;
  }

  getCurrentTheme(): string {
    return localStorage.getItem('theme') || 'aura-light';
  }
}
