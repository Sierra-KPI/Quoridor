# Quoridor

<p align="center">
  <img src="https://github.com/Sierra-KPI/Quoridor/blob/main/docs/Quoridor.jpg" data-canonical-src="https://github.com/Sierra-KPI/Quoridor/blob/main/docs/Quoridor.jpg" />
</p>

## Table of Contents

- [Description](#description)
- [Badges](#badges)
- [Contributing](#contributing)
- [License](#license)

### Description

The abstract strategy game Quoridor is surprisingly deep for its simple rules. The object of the game is to advance your pawn to the opposite edge of the board. On your turn you may either move your pawn or place a wall. You may hinder your opponent with wall placement, but not completely block them off. Meanwhile, they are trying to do the same to you. The first pawn to reach the opposite side wins.

## Badges



---

## Example (Optional)

```csharp
/// <summary>
	/// Class for Api Client
	/// </summary>
	public static class ApiHelper
	{
		// Create static, 'cause We need one client per application
		public static HttpClient ApiClient { get; set; }

		/// <summary>
		/// Initializes API client
		/// </summary>
		public static void Initialize()
		{
			ApiClient = new HttpClient
			{
				// a lot of adresses will begin with the same string,
				// so We can put the beginning here
				// but won't, because We need more than one adress
				/*
				BaseAddress = new Uri("http://somesite.com/")
				*/
			};
			ApiClient.DefaultRequestHeaders.Accept.Clear();
			// give Us json, not webpage or etc.
			ApiClient.DefaultRequestHeaders.Accept.Add(new
				MediaTypeWithQualityHeaderValue("application/json"));
		}
	}
```

---

## Contributing

> To get started...

### Step 1

- 🍴 Fork this repo!

### Step 2

- **HACK AWAY!** 🔨🔨🔨

---

## License

[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](http://badges.mit-license.org)

- **[MIT license](http://opensource.org/licenses/mit-license.php)**
- Copyright 2021 © <a href="https://github.com/VsIG-official" target="_blank">VsIG</a>.
