import { useState } from 'react';
import InputError from '../../components/input-error';
import TextLink from '../../components/text-link';
import { Button } from '../../components/ui/button';
import { Input } from '../../components/ui/input';
import { Label } from '../../components/ui/label';
import AuthLayout from '../../layouts/auth-layout';

type LoginForm = {
	email: string;
	password: string;
	remember: boolean;
};

interface LoginProps {
	status?: string;
	canResetPassword: boolean;
}

const Login = ({ status, canResetPassword }: LoginProps) => {
	const [form, setForm] = useState<LoginForm>({
		email: '',
		password: '',
	});

	const [isLoading, setLoading] = useState<boolean>(false);
	const [errors, setError] = useState(null);

	function submit(e) {
		e.preventDefault();
	}

	return (
		<AuthLayout title="Log in to your account" description="Enter your email and password below to log in">
			<Head title="Log in" />

			<form className="flex flex-col gap-6" onSubmit={submit}>
				<div className="grid gap-6">
					<div className="grid gap-2">
						<Label htmlFor="email">Email address</Label>
						<Input
							id="email"
							type="email"
							required
							autoFocus
							tabIndex={1}
							autoComplete="email"
							value={form.email}
							onChange={(e) => setForm('email', e.target.value)}
							placeholder="email@example.com"
						/>
						<InputError message={errors.email} />
					</div>

					<div className="grid gap-2">
						<div className="flex items-center">
							<Label htmlFor="password">Password</Label>
							{canResetPassword && (
								<TextLink href={route('password.request')} className="ml-auto text-sm" tabIndex={5}>
									Forgot password?
								</TextLink>
							)}
						</div>
						<Input
							id="password"
							type="password"
							required
							tabIndex={2}
							autoComplete="current-password"
							value={form.password}
							onChange={(e) => setForm('password', e.target.value)}
							placeholder="Password"
						/>
						<InputError message={errors.password} />
					</div>

					<div className="flex items-center space-x-3">
						<Checkbox id="remember" name="remember" checked={form.remember} onClick={() => setForm('remember', !form.remember)} tabIndex={3} />
						<Label htmlFor="remember">Remember me</Label>
					</div>

					<Button type="submit" className="mt-4 w-full" tabIndex={4} disabled={isLoading}>
						{isLoading && <LoaderCircle className="h-4 w-4 animate-spin" />}
						Log in
					</Button>
				</div>

				<div className="text-muted-foreground text-center text-sm">
					Don't have an account?{' '}
					<TextLink to="/register" tabIndex={5}>
						Sign up
					</TextLink>
				</div>
			</form>

			{status && <div className="mb-4 text-center text-sm font-medium text-green-600">{status}</div>}
		</AuthLayout>
	);
};

export default Login;
