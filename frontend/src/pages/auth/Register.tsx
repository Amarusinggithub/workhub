import { useState } from 'react';
import InputError from '../../components/input-error';
import TextLink from '../../components/text-link';
import { Button } from '../../components/ui/button';
import { Input } from '../../components/ui/input';
import { Label } from '../../components/ui/label';
import AuthLayout from '../../layouts/auth-layout';


type ResgisterProps = {
	firstName: string;
	lastName: string;
	email: string;
	password: string;
	confirmPassword: string;
};

const Register = () => {
	const [form, setForm] = useState<ResgisterProps>({
		firstName: '',
		lastName: '',
		email: '',
		password: '',
		confirmPassword: '',
	});

    const[isLoading,setLoading]=useState<boolean>(false);
    const[error,setError]=useState(null);

	function submit(e: React.FormEventHandler<HTMLFormElement> ) {
		e.preventDefault();
	}

	return (
		<>
			<form className="flex flex-col gap-6" onSubmit={(e)=>submit(e)}>
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
                            value={form.name}
                            onChange={(e) => setForm('firstName', e.target.value)}
                            disabled={isLoading}
                            placeholder="First Name"
                        />
                        <InputError message={errors.name} className="mt-2" />
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
							value={form.name}
							onChange={(e) => setForm('lastName', e.target.value)}
							disabled={isLoading}
							placeholder="last Name"
						/>
						<InputError message={errors.name} className="mt-2" />
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
							onChange={(e) => setForm('email', e.target.value)}
							disabled={isLoading}
							placeholder="email@example.com"
						/>
						<InputError message={errors.email} />
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
							onChange={(e) => setForm('password', e.target.value)}
							disabled={isLoading}
							placeholder="Password"
						/>
						<InputError message={errors.password} />
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
							onChange={(e) => setForm('confirmPassword', e.target.value)}
							disabled={isLoading}
							placeholder="Confirm password"
						/>
						<InputError message={error.confirmPassword} />
					</div>

					<Button type="submit" className="mt-2 w-full" tabIndex={5} disabled={processing}>
						{isLoading && <LoaderCircle className="h-4 w-4 animate-spin" />}
						Create account
					</Button>
				</div>

				<div className="text-muted-foreground text-center text-sm">
					Already have an account?{' '}
					<TextLink href={} tabIndex={6}>
						Log in
					</TextLink>
				</div>
			</form>
		</>
	);
};

export default Register;
