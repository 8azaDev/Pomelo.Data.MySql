# Pomelo.Data.MySql

[![Travis build status](https://img.shields.io/travis/PomeloFoundation/Pomelo.Data.MySql.svg?label=travis-ci&branch=master&style=flat-square)](https://travis-ci.org/PomeloFoundation/Pomelo.Data.MySql)
[![AppVeyor build status](https://img.shields.io/appveyor/ci/Kagamine/Pomelo-Data-MySql/master.svg?label=appveyor&style=flat-square)](https://ci.appveyor.com/project/Kagamine/pomelo-data-mysql/branch/master) [![NuGet](https://img.shields.io/nuget/v/Pomelo.Data.MySql.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/Pomelo.Data.MySql/)

Contains MySQL implementations of the System.Data.Common(Both .NET Core and .NET Framework) interfaces.

## Getting Started

To add the `Pomelo.Data.MySql` into your `project.json`:

```json
{
  "version": "1.0.0-*",
  "buildOptions": {
    "emitEntryPoint": true
  },

  "dependencies": {
    "Microsoft.NETCore.App": {
      "type": "platform",
      "version": "1.0.0"
    },
    "Pomelo.Data.MySql": "1.0.0-prerelease-20160726"
  },

  "frameworks": {
    "netcoreapp1.0": {
      "imports": "dnxcore50"
    }
  }
}
```

`MySqlConnection`, `MySqlCommand` and etc were included in the namespace `Pomelo.Data.MySql`. The following console application sample will show you how to use this library to write a record into MySQL database.

```C#
using Pomelo.Data.MySql;

namespace MySqlAdoSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var conn = new MySqlConnection("server=localhost;database=adosample;uid=root;pwd=yourpwd"))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("INSERT INTO `test` (`content`) VALUES ('Hello MySQL')", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
```

## Contribute

One of the easiest ways to contribute is to participate in discussions and discuss issues. You can also contribute by submitting pull requests with code changes.

## License

[MIT](https://github.com/PomeloFoundation/Pomelo.Data.MySql/blob/master/LICENSE)
