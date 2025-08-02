import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import App from './App.tsx';
import { initializeTheme } from './hooks/use-appearance.tsx';
import './index.css';

// This will set light / dark mode on load...
initializeTheme();

createRoot(document.getElementById('root')!).render(
	<StrictMode>
		<App />
	</StrictMode>,
);
