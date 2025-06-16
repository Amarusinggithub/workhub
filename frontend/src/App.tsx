import './App.css';
import { initializeTheme } from './hooks/use-appearance';
import { AuthProvider } from './hooks/use-auth';
import AppRoutes from './routes/app-routes';

function App() {
	return (
		<AuthProvider>
			<AppRoutes />
		</AuthProvider>
	);
}
// This will set light / dark mode on load...
initializeTheme();

export default App;
