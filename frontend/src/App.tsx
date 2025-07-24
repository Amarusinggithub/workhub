import { ThemeProvider } from './hooks/use-theme';
import './App.css';
import  AuthProvider  from './hooks/use-auth';
import AppRoutes from './routes/app-routes';
import { ErrorBoundary } from 'react-error-boundary';
import ErrorFallback from './pages/error';



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
