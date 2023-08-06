# Author: Patryk Lesiak
# Date: 2023-08-01
# In case of any questions, please contact me via Discord (vlesio)

# This script is used for installing Coin Additions

import os
import sys
import subprocess
import logging

def help():
    print(f'Usage: python coinadds.py <command>')
    print(f'Commands:')
    print('setup <addition> - sets up selected addition for your project')
    print('    example: setup git-hooks')

def setup():
    if len(sys.argv) < 3:
        help()
        sys.exit()
    
    output = subprocess.run(['python', f'./.coin/{sys.argv[2]}/install.py'], text=True, capture_output=True)
    if output.returncode == 0:
        print(f'{output.stdout.strip()}')
    else:
        print(f'{output.stderr.strip()}')
        print(f'No such addition: {sys.argv[2]}')

# Switch to project directory
os.chdir(os.path.dirname(__file__)+'/..')

print('Coin Additions')

if len(sys.argv) == 1:
    help()
elif sys.argv[1] == "setup":
    setup()
else:
    help()
