import { ErrorBoundary } from 'react-error-boundary';
import './App.css';
import AuthProvider from './hooks/use-auth';
import { ThemeProvider } from './hooks/use-theme';
import ErrorFallback from './pages/error';
import AppRoutes from './routes/app-routes';

function App() {
	return (
		<ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
			<ErrorBoundary FallbackComponent={ErrorFallback}>
				<AuthProvider>
					<AppRoutes />
				</AuthProvider>
			</ErrorBoundary>
		</ThemeProvider>
	);
}

export default App;
