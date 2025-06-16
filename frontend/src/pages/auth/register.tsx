import { useState } from 'react';
import InputError from '../../components/input-error';
import TextLink from '../../components/text-link';
import { Button } from '../../components/ui/button';
import { Input } from '../../components/ui/input';
import { Label } from '../../components/ui/label';
import AuthLayout from '../../layouts/auth-layout';
import { LoaderCircle } from 'lucide-react';
import useAuth from '../../hooks/use-auth';

type RegisterForm = {
	firstName: string;
	lastName: string;
	email: string;
	password: string;
	confirmPassword: string;
};

const Register = () => {
	const [form, setForm] = useState<RegisterForm>({
		firstName: '',
		lastName: '',
		email: '',
		password: '',
		confirmPassword: '',
	});

    const{isLoading,errors}=useAuth();
    function change(e: React.ChangeEvent<HTMLInputElement>) {
			setForm({ ...form, [e.target.name]: e.target.value.trim() });
		}


	function submit(e: React.FormEvent<HTMLFormElement>) {
		e.preventDefault();
	}

	return (
		<AuthLayout title="Create an account" description="Enter your details below to create your account">
			<h1>Register</h1>
			<form className="flex flex-col gap-6" onSubmit={(e) => submit(e)}>
				<div className="grid gap-6">
					<div className="grid gap-2">
						<Label htmlFor="firstname">First Name</Label>
						<Input
							id="firstname"
							type="text"
							required
							autoFocus
							tabIndex={1}
							autoComplete="firstName"
							value={form.firstName}
							onChange={(e) => change(e)}
							disabled={isLoading}
							placeholder="First Name"
						/>
						<InputError message={errors} className="mt-2" />
					</div>
					<div className="grid gap-2">
						<Label htmlFor="lastName">Last Name</Label>
						<Input
							id="lastName"
							type="text"
							required
							autoFocus
							tabIndex={2}
							autoComplete="lastName"
							value={form.lastName}
							onChange={(e) => change(e)}
							disabled={isLoading}
							placeholder="last Name"
						/>
						<InputError message={errors} className="mt-2" />
					</div>

					<div className="grid gap-2">
						<Label htmlFor="email">Email address</Label>
						<Input
							id="email"
							type="email"
							required
							tabIndex={3}
							autoComplete="email"
							value={form.email}
							onChange={(e) => change(e)}
							disabled={isLoading}
							placeholder="email@example.com"
						/>
						<InputError message={errors} />
					</div>

					<div className="grid gap-2">
						<Label htmlFor="password">Password</Label>
						<Input
							id="password"
							type="password"
							required
							tabIndex={4}
							autoComplete="new-password"
							value={form.password}
							onChange={(e) => change(e)}
							disabled={isLoading}
							placeholder="Password"
						/>
						<InputError message={errors} />
					</div>

					<div className="grid gap-2">
						<Label htmlFor="password_confirmation">Confirm password</Label>
						<Input
							id="password_confirmation"
							type="password"
							required
							tabIndex={5}
							autoComplete="new-password"
							value={form.confirmPassword}
							onChange={(e) => change(e)}
							disabled={isLoading}
							placeholder="Confirm password"
						/>
						<InputError message={errors} />
					</div>

					<Button type="submit" className="mt-2 w-full" tabIndex={5} disabled={isLoading}>
						{isLoading && <LoaderCircle className="h-4 w-4 animate-spin" />}
						Create account
					</Button>
				</div>

				<div className="text-muted-foreground text-center text-sm">
					Already have an account? <TextLink to={"/login"}tabIndex={6}>Log in</TextLink>
				</div>
			</form>
		</AuthLayout>
	);
};

export default Register;
